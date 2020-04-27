using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Liquid.Interfaces;
using Liquid.OnAzure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Liquid.Runtime.Tests
{
    public class AzureRedisTest : IDisposable
    {
        private const string ContentType = "text/plain";
        private const string DefaultConnectionString = "UseDevelopmentStorage=true";
        private const string DefaultContainerName = "removecontainer";
        private static readonly IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        private static readonly ILightCache _fakeLightCache = Substitute.For<ILightCache>();

        private readonly string _expectedData;

        private readonly Stream _stream;

        //private readonly ILightAttachment _lightAttachment;

        private readonly AzureRedis _sut;

        public AzureRedisTest()
        {
            Workbench.Instance.Reset();

            Workbench.Instance.AddToCache(WorkbenchServiceType.Repository, _fakeLightCache);

            _sut = new AzureRedis()
            {

            };

            _expectedData = _fixture.Create<string>();
            
            //FALTA RESTO DA IMPLEMENTAÇÃO
            
            // _stream = ToMemoryStream(_expectedData);

            //_lightAttachment = new LightAttachment
            //{
            //    ContentType = ContentType,
            //    Id = _fixture.Create<string>() + ".txt",
            //    MediaLink = _fixture.Create<string>(),
            //    MediaStream = _stream,
            //    Name = _fixture.Create<string>(),
            //    ResourceId = _fixture.Create<string>(),
            //};
        }

        [Fact]
        public void CtorWorkingProprely()
        {
            ////Arrange
            //var sut = new AzureRedis();
            //var expected = new AzureRedis();
            
            ////Act
            //var result = sut.
            
            ////Assert
            //Assert.Equal(expected, _sut);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
