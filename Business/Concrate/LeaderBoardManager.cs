using System;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using Entitiy.Concrate;
using Entitiy.Dtos;
using MongoDB.Bson;

namespace Business.Concrate
{
    public class LeaderBoardManager : ILeaderBoardService
    {
        private readonly IUserDao _userDal;
        private readonly IPointDao _pointsDao;
        private readonly ILeaderBoardDao _leaderBoardDao;

        public LeaderBoardManager(IUserDao userDal, IPointDao pointsDao, ILeaderBoardDao leaderBoardDao)
        {
            _userDal = userDal;
            _pointsDao = pointsDao;
            _leaderBoardDao = leaderBoardDao;
        }

        private IResult CheckIsExistInMonth()
        {
            if (_leaderBoardDao.GetAll().Any(x => x.CreateDate.Month == DateTime.Now.Month))
            {
                return new ErrorResult("Bu ay için leader board zaten oluşturuldu.");
            }

            return new SuccessResult();
        }

        private string getAward(int rank, int consolationPrice)
        {
            switch (rank)
            {
                case 1:
                    return "First Prize";
                case 2:
                    return "Second Prize";
                case 3:
                    return "Third Prize";
                case <= 100:
                    return "25$";
                case <= 1000:
                    return $"Consolation prize - {consolationPrice}$";
                default:
                    return "Ne yazık ki ödül kazanamadınız.";
            }
        }

        private List<LeaderBoards> getRank(List<LeaderBoards> boards)
        {
            var rank = 1;
            foreach (var board in boards)
            {
                board.Rank = rank;
                rank++;
            }
            return boards;
        }

        public IResult CreateLeaderBoard()
        {
            var userPoints = _pointsDao.GetAll(x => x.approved == true).GroupBy(point => point.user_id).Select(group => new
            LeaderBoards()
            {
                user_id = group.Key,
                TotalPoints = group.Sum(x => x.point),
                CreateDate = DateTime.Now
            }).OrderByDescending(x => x.TotalPoints).ToList();


            var result = BusinessRules.Run(CheckIsExistInMonth());
            if (result != null && !result.Success) return result;

            getRank(userPoints);

            var restUsers = userPoints.Count(x => x.Rank > 100 && x.Rank <= 1000);
            var consolationPrice = 12500 / (restUsers > 0 ? restUsers : 1);

            foreach (var board in userPoints)
            {
                board.Award = getAward(board.Rank, consolationPrice);
                _leaderBoardDao.AddAsync(board);
            }
            return new SuccessResult();
        }

        public IDataResult<List<UserAwardDto>> GetUsersAward(ObjectId userId)
        {
            return new SuccessDataResult<List<UserAwardDto>>(_leaderBoardDao.GetUserAwarDto(userId));
        }

        public IDataResult<List<LeaderBoards>> GetAllWithMonthOrUserId(int? month, ObjectId? userId)
        {
            return new SuccessDataResult<List<LeaderBoards>>
                (_leaderBoardDao.GetAll().Where(x => (month == null || x.CreateDate.Month == month) && (userId == null || userId == x.user_id)).ToList());
        }
    }
}

