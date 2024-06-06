using FsCheck;
using FSharp.PropertyTesting;
using NSubstitute;

namespace CSharp.PropertyTesting.Tests;

public class PropertyTests
{
    private readonly IRepository _repository = Substitute.For<IRepository>();
    private readonly MyServiceWorking _sut;

    public PropertyTests()
    {
        _sut = new MyServiceWorking(_repository);
    }

    [Fact]
    public void RemovesAllBeforeCutoff()
    {
        Prop.ForAll((List<MyEntity> data, int cutoff) =>
            {
                _repository.List().Returns(data);

                var result = _sut.AllAfterYear(cutoff);

                Assert.DoesNotContain(result, x => x.CreatedOn.Year < cutoff);
            })
            .QuickCheck();
    }
}
