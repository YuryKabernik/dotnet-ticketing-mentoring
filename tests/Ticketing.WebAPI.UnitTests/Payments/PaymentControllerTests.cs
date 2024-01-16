using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Application.Feature.Payments.Responses;
using Ticketing.WebApi.Models;
using Ticketing.WebApi.Payments;

public class PaymentControllerTests
{
    private readonly Guid _testPaymentId = Guid.NewGuid();
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    private readonly Mock<IMediator> _mediatorMock;
    private readonly PaymentController _controller;

    public PaymentControllerTests()
    {
        this._mediatorMock = new Mock<IMediator>();
        this._controller = new PaymentController(this._mediatorMock.Object);
    }

    [Fact]
    public async Task GetStatus_ReturnsPaidStatus()
    {
        this._mediatorMock
            .Setup(m => m.Send(It.IsAny<PaymentByIdRequest>(), this._cancellationToken))
            .ReturnsAsync(new PaymentStatusResponse("Paid"));

        Ok<PaymentStatus>? result = await this._controller.GetStatus(this._testPaymentId, this._cancellationToken) as Ok<PaymentStatus>;

        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        Assert.Equal("Paid", result.Value.Status);
        this.AssertStatusRequestSent();
    }

    private void AssertStatusRequestSent()
    {
        this._mediatorMock.Verify(
            m => m.Send(It.Is<PaymentByIdRequest>(x => x.PaymentId == this._testPaymentId), this._cancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateToComplete_SendsCompleteCommand()
    {
        IResult result = await this._controller.UpdateToComplete(this._testPaymentId, this._cancellationToken);

        Assert.IsType<Ok>(result);
        this.AssertCompleteRequestSent();
    }

    private void AssertCompleteRequestSent()
    {
        this._mediatorMock.Verify(
            m => m.Send(It.Is<CompletePaymentRequest>(x => x.PaymentId == this._testPaymentId), this._cancellationToken),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateToFailed_SendsFailCommand()
    {
        IResult result = await this._controller.UpdateToFailed(this._testPaymentId, this._cancellationToken);

        Assert.IsType<Ok>(result);
        this.AssertFailRequestSent();
    }

    private void AssertFailRequestSent()
    {
        this._mediatorMock.Verify(
            m => m.Send(It.Is<FailPaymentRequest>(x => x.PaymentId == this._testPaymentId), this._cancellationToken),
            Times.Once
        );
    }
}