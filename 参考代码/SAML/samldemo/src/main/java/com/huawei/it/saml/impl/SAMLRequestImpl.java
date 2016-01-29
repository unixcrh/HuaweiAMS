package com.huawei.it.saml.impl;

import com.huawei.it.saml.SAMLException;
import com.huawei.it.saml.SAMLRequest;
import com.huawei.it.saml.ServiceProvider;
import com.huawei.it.saml.response.RequestBuilder;

public class SAMLRequestImpl implements SAMLRequest {
	@Override
	public String generate() throws SAMLException {
		ServiceProvider sp = new ServiceProviderImpl();
		return new RequestBuilder(sp.getIssuer(), sp.getServiceUrl(),
				sp.getPrivateKeyEntry()).build();
	}
}
