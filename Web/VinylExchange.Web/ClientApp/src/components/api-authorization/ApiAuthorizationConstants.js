export const ApplicationName = "VinylExchange";

export const QueryParameterNames = {
  ReturnUrl: "returnUrl",
  Message: "message"
};

export const LogoutActions = {
  Logout: "Logout"
};

export const LoginActions = {
  Login: "Login",
  LoginCallback: "Login-Callback",
  
};

const prefix = "/Authentication";

export const ApplicationPaths = {
  DefaultLoginRedirectPath: "/",
  ApiAuthorizationClientConfigurationUrl: `/_configuration/${ApplicationName}`,
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${LoginActions.Login}`,
  LoginCallback: `${prefix}/${LoginActions.LoginCallback}`
};
