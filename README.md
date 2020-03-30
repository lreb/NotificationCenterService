# Notification Center Service

## Services AWS

Basi resources to run the application in aws console

### Lambda

Use roles instead of IAM keys (Controllers)

NotificationCenterService::NotificationCenterService.LambdaEntryPoint::FunctionHandlerAsync

### API Gateway

- Root resource
	- ANY (LAMBDA_PROXY)
		- api
			- notifications
				- POST (LAMBDA_PROXY)

