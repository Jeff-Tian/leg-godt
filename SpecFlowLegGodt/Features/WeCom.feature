Feature: WeCom
	 企业微信相关的特性
	
@wecom
Scenario: 获取访问令牌
	Given the enterprise is "hardway"
	Then the current token is "abc"