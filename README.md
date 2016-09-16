# Innovation Days 2016 AAD B2C Demo
Demo code from Innovation Days 2016 talk on Azure AD B2C

In order to get this working you will need an Azure Subscription and have provisioned:

* An AAD B2C tenant
  * Setup IdP and Policies
* An App Service Plan
  * A Web App
  * An API App
* Two App Insights instances

Then edit the following files:

ApplicationInsights.config - unique GUID per project to log to different services.
InstrumentationKey and replace YOUR-KEY-HERE with valid App Insights intrumentation key.

Web App: web.config edits:

B2C:
Tenant;
ClientId;
Create policies in tenant.

Document DB: 
endpoint;
authKey;

API app: web.config

B2C
Tenant - same as Web;
ClientId - different from Web;
Create policies in tenant.

Document DB: 
endpoint - same as web;
authKey - same as web.
