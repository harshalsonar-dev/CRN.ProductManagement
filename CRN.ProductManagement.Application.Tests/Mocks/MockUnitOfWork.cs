using Moq;
using CRN.ProductManagement.Application.Interfaces;

namespace CRN.ProductManagement.Application.Tests.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var mockProductRepository = new Mock<IProductRepository>();

        mockUnitOfWork
            .Setup(x => x.Products)
            .Returns(mockProductRepository.Object);

        mockUnitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        return mockUnitOfWork;
    }
}