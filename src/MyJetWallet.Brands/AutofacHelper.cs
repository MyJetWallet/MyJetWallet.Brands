using System;
using Autofac;
using MyJetWallet.Brands.NoSql;
using MyJetWallet.Sdk.NoSql;
using MyNoSqlServer.DataReader;
using MyNoSqlServer.DataWriter.Builders;

namespace MyJetWallet.Brands
{
    public static class AutofacHelper
    {
        public static ContainerBuilder RegisterBrandManager(this ContainerBuilder builder,
            Func<string> myNoSqlWriterUrl)
        {
            builder.RegisterMyNoSqlWriter<BrandNoSql>(myNoSqlWriterUrl, BrandNoSql.TableName, true);
            builder.RegisterType<BrandManager>().As<IBrandManager>().AutoActivate().SingleInstance();
            
            return builder;
        }
        
        public static ContainerBuilder RegisterBrandReader(this ContainerBuilder builder,
            IMyNoSqlSubscriber client)
        {
            builder.RegisterMyNoSqlReader<BrandNoSql>(client, BrandNoSql.TableName);
            builder.RegisterType<BrandReader>().As<IBrandReader>().AutoActivate().SingleInstance();
            
            return builder;
        }
    }
}