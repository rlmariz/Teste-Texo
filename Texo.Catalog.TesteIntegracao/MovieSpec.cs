using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Teste_Texo_Api;
using Xunit;

namespace Texo.Catalog.TesteIntegracao
{
    public class MovieSpec
    {
        private readonly TestContext testContext;

        public MovieSpec()
        {
            testContext = new TestContext();
        }

        public async Task<T> ResponseToObjectAsync<T>(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();
            T ret = serializer.Deserialize<T>(jsonReader);
            return ret;
        }

        [Theory]
        [InlineData("/movie")]
        [InlineData("/movie/1")]
        [InlineData("/movie/winners")]
        [InlineData("/movie/wins")]
        [InlineData("/movie/winsstatistic")]
        public async Task EndPointsSpec(string url)
        {
            var response = await testContext.Client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllSpecAsync()
        {
            var response = await testContext.Client.GetAsync("/movie");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = ResponseToObjectAsync<List<MovieItem>>(response);
            task.Result.Count.Should().Be(20);
        }

        [Fact]
        public async Task GetIdSpecAsync()
        {
            var response = await testContext.Client.GetAsync("/movie/1");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = ResponseToObjectAsync<MovieItem>(response);
            task.Result.Title.Should().Be("Title 01");
        }

        [Fact]
        public async Task GetiWnnersSpecAsync()
        {
            var response = await testContext.Client.GetAsync("/movie/winners");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = ResponseToObjectAsync<List<MovieItem>>(response);
            task.Result.Count.Should().Be(12);
        }

        [Fact]
        public async Task GetWinsStatisticSpecAsync()
        {
            var response = await testContext.Client.GetAsync("/movie/winsstatistic");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = ResponseToObjectAsync<ProducerWinsStatisticListModel>(response);
            var producerWinsStatisticListModel = task.Result;

            producerWinsStatisticListModel.Min.Count.Should().Be(3);
            foreach (var producerWinsStatistic in producerWinsStatisticListModel.Min)
            {
                producerWinsStatistic.Interval.Should().Be(1);
            }
            producerWinsStatisticListModel.Min[0].Producer.Should().Be("Producer 7");
            producerWinsStatisticListModel.Min[1].Producer.Should().Be("Producer 8");
            producerWinsStatisticListModel.Min[2].Producer.Should().Be("Producer 9");

            producerWinsStatisticListModel.Max.Count.Should().Be(2);
            foreach (var producerWinsStatistic in producerWinsStatisticListModel.Max)
            {
                producerWinsStatistic.Interval.Should().Be(15);
            }
            producerWinsStatisticListModel.Max[0].Producer.Should().Be("Producer 1");
            producerWinsStatisticListModel.Max[1].Producer.Should().Be("Producer 2");
        }
    }
}
