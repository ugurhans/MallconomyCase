using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrate;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrate.MongoDb;

namespace Business.DependencyResolver
{
    public class AutoFacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<LeaderBoardManager>().As<ILeaderBoardService>().SingleInstance();
            builder.RegisterType<MongoLeaderBoardDal>().As<ILeaderBoardDao>().SingleInstance();

            builder.RegisterType<MongoPointDal>().As<IPointDao>().SingleInstance();
            //builder.RegisterType<PointManager>().As<IPointService>().SingleInstance();

            builder.RegisterType<MongoUserDal>().As<IUserDao>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
              .EnableInterfaceInterceptors(new ProxyGenerationOptions()
              {
                  Selector = new AspectInterceptorSelector()
              }).SingleInstance();

        }


    }
}

