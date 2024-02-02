using Amazon.Runtime;
using Amazon.Runtime.Endpoints;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace ApiTest.Fixtures;

public class MockEmailClient:IAmazonSimpleEmailService
{
    public IClientConfig Config
    {
        get;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<CloneReceiptRuleSetResponse> CloneReceiptRuleSetAsync(CloneReceiptRuleSetRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateConfigurationSetResponse> CreateConfigurationSetAsync(CreateConfigurationSetRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateConfigurationSetEventDestinationResponse> CreateConfigurationSetEventDestinationAsync(CreateConfigurationSetEventDestinationRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateConfigurationSetTrackingOptionsResponse> CreateConfigurationSetTrackingOptionsAsync(CreateConfigurationSetTrackingOptionsRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateCustomVerificationEmailTemplateResponse> CreateCustomVerificationEmailTemplateAsync(CreateCustomVerificationEmailTemplateRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateReceiptFilterResponse> CreateReceiptFilterAsync(CreateReceiptFilterRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateReceiptRuleResponse> CreateReceiptRuleAsync(CreateReceiptRuleRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateReceiptRuleSetResponse> CreateReceiptRuleSetAsync(CreateReceiptRuleSetRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<CreateTemplateResponse> CreateTemplateAsync(CreateTemplateRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteConfigurationSetResponse> DeleteConfigurationSetAsync(DeleteConfigurationSetRequest request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteConfigurationSetEventDestinationResponse> DeleteConfigurationSetEventDestinationAsync(DeleteConfigurationSetEventDestinationRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteConfigurationSetTrackingOptionsResponse> DeleteConfigurationSetTrackingOptionsAsync(DeleteConfigurationSetTrackingOptionsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteCustomVerificationEmailTemplateResponse> DeleteCustomVerificationEmailTemplateAsync(DeleteCustomVerificationEmailTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteIdentityResponse> DeleteIdentityAsync(DeleteIdentityRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteIdentityPolicyResponse> DeleteIdentityPolicyAsync(DeleteIdentityPolicyRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteReceiptFilterResponse> DeleteReceiptFilterAsync(DeleteReceiptFilterRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteReceiptRuleResponse> DeleteReceiptRuleAsync(DeleteReceiptRuleRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteReceiptRuleSetResponse> DeleteReceiptRuleSetAsync(DeleteReceiptRuleSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteTemplateResponse> DeleteTemplateAsync(DeleteTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DeleteVerifiedEmailAddressResponse> DeleteVerifiedEmailAddressAsync(DeleteVerifiedEmailAddressRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DescribeActiveReceiptRuleSetResponse> DescribeActiveReceiptRuleSetAsync(DescribeActiveReceiptRuleSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DescribeConfigurationSetResponse> DescribeConfigurationSetAsync(DescribeConfigurationSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DescribeReceiptRuleResponse> DescribeReceiptRuleAsync(DescribeReceiptRuleRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<DescribeReceiptRuleSetResponse> DescribeReceiptRuleSetAsync(DescribeReceiptRuleSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetAccountSendingEnabledResponse> GetAccountSendingEnabledAsync(GetAccountSendingEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetCustomVerificationEmailTemplateResponse> GetCustomVerificationEmailTemplateAsync(GetCustomVerificationEmailTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetIdentityDkimAttributesResponse> GetIdentityDkimAttributesAsync(GetIdentityDkimAttributesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetIdentityMailFromDomainAttributesResponse> GetIdentityMailFromDomainAttributesAsync(GetIdentityMailFromDomainAttributesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetIdentityNotificationAttributesResponse> GetIdentityNotificationAttributesAsync(GetIdentityNotificationAttributesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetIdentityPoliciesResponse> GetIdentityPoliciesAsync(GetIdentityPoliciesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetIdentityVerificationAttributesResponse> GetIdentityVerificationAttributesAsync(GetIdentityVerificationAttributesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetSendQuotaResponse> GetSendQuotaAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetSendQuotaResponse> GetSendQuotaAsync(GetSendQuotaRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetSendStatisticsResponse> GetSendStatisticsAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetSendStatisticsResponse> GetSendStatisticsAsync(GetSendStatisticsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<GetTemplateResponse> GetTemplateAsync(GetTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListConfigurationSetsResponse> ListConfigurationSetsAsync(ListConfigurationSetsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListCustomVerificationEmailTemplatesResponse> ListCustomVerificationEmailTemplatesAsync(ListCustomVerificationEmailTemplatesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListIdentitiesResponse> ListIdentitiesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListIdentitiesResponse> ListIdentitiesAsync(ListIdentitiesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListIdentityPoliciesResponse> ListIdentityPoliciesAsync(ListIdentityPoliciesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListReceiptFiltersResponse> ListReceiptFiltersAsync(ListReceiptFiltersRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListReceiptRuleSetsResponse> ListReceiptRuleSetsAsync(ListReceiptRuleSetsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListTemplatesResponse> ListTemplatesAsync(ListTemplatesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListVerifiedEmailAddressesResponse> ListVerifiedEmailAddressesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ListVerifiedEmailAddressesResponse> ListVerifiedEmailAddressesAsync(ListVerifiedEmailAddressesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<PutConfigurationSetDeliveryOptionsResponse> PutConfigurationSetDeliveryOptionsAsync(PutConfigurationSetDeliveryOptionsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<PutIdentityPolicyResponse> PutIdentityPolicyAsync(PutIdentityPolicyRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ReorderReceiptRuleSetResponse> ReorderReceiptRuleSetAsync(ReorderReceiptRuleSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SendBounceResponse> SendBounceAsync(SendBounceRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SendBulkTemplatedEmailResponse> SendBulkTemplatedEmailAsync(SendBulkTemplatedEmailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SendCustomVerificationEmailResponse> SendCustomVerificationEmailAsync(SendCustomVerificationEmailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        return new SendEmailResponse();
    }

    public Task<SendRawEmailResponse> SendRawEmailAsync(SendRawEmailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SendTemplatedEmailResponse> SendTemplatedEmailAsync(SendTemplatedEmailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetActiveReceiptRuleSetResponse> SetActiveReceiptRuleSetAsync(SetActiveReceiptRuleSetRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetIdentityDkimEnabledResponse> SetIdentityDkimEnabledAsync(SetIdentityDkimEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetIdentityFeedbackForwardingEnabledResponse> SetIdentityFeedbackForwardingEnabledAsync(SetIdentityFeedbackForwardingEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetIdentityHeadersInNotificationsEnabledResponse> SetIdentityHeadersInNotificationsEnabledAsync(SetIdentityHeadersInNotificationsEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetIdentityMailFromDomainResponse> SetIdentityMailFromDomainAsync(SetIdentityMailFromDomainRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetIdentityNotificationTopicResponse> SetIdentityNotificationTopicAsync(SetIdentityNotificationTopicRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SetReceiptRulePositionResponse> SetReceiptRulePositionAsync(SetReceiptRulePositionRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<TestRenderTemplateResponse> TestRenderTemplateAsync(TestRenderTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateAccountSendingEnabledResponse> UpdateAccountSendingEnabledAsync(UpdateAccountSendingEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateConfigurationSetEventDestinationResponse> UpdateConfigurationSetEventDestinationAsync(UpdateConfigurationSetEventDestinationRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateConfigurationSetReputationMetricsEnabledResponse> UpdateConfigurationSetReputationMetricsEnabledAsync(UpdateConfigurationSetReputationMetricsEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateConfigurationSetSendingEnabledResponse> UpdateConfigurationSetSendingEnabledAsync(UpdateConfigurationSetSendingEnabledRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateConfigurationSetTrackingOptionsResponse> UpdateConfigurationSetTrackingOptionsAsync(UpdateConfigurationSetTrackingOptionsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateCustomVerificationEmailTemplateResponse> UpdateCustomVerificationEmailTemplateAsync(UpdateCustomVerificationEmailTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateReceiptRuleResponse> UpdateReceiptRuleAsync(UpdateReceiptRuleRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<UpdateTemplateResponse> UpdateTemplateAsync(UpdateTemplateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<VerifyDomainDkimResponse> VerifyDomainDkimAsync(VerifyDomainDkimRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<VerifyDomainIdentityResponse> VerifyDomainIdentityAsync(VerifyDomainIdentityRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<VerifyEmailAddressResponse> VerifyEmailAddressAsync(VerifyEmailAddressRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<VerifyEmailIdentityResponse> VerifyEmailIdentityAsync(VerifyEmailIdentityRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Endpoint DetermineServiceOperationEndpoint(AmazonWebServiceRequest request)
    {
        throw new NotImplementedException();
    }

    public ISimpleEmailPaginatorFactory Paginators
    {
        get;
    }
}
