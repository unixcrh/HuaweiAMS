<%@page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<html>
<head>
<title>Login</title>
</head>
<body>
	<form id="loginForm" name="loginForm" method="post"
		action="https://uniportal-beta.huawei.com/saaslogin/sp">
		<textarea id="SAMLRequest" name="SAMLRequest" cols="80" rows="30">${SAMLRequest}</textarea>
		<input id="RelayState" name="RelayState" type="hidden" value="${RelayState}" />
		<!-- <input name="SAMLRequest" type="hidden" value="${SAMLReqiest}" /> -->
		<p>
			<input type="submit" id="singin" value="Sign In" />
		</p>
	</form>
</body>
</html>