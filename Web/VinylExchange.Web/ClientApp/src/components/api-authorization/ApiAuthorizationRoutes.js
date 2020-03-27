import React, { Component, Fragment } from 'react';
import { Route } from 'react-router';
import { Login } from './Login'
import { ApplicationPaths, LoginActions, LogoutActions } from './ApiAuthorizationConstants';

export default class ApiAuthorizationRoutes extends Component {

  render () {
    return(
      <Fragment>
          <Route path={ApplicationPaths.Login} render={() => loginAction(LoginActions.Login)} />
          <Route path={ApplicationPaths.LoginFailed} render={() => loginAction(LoginActions.LoginFailed)} />
          <Route path={ApplicationPaths.LoginCallback} render={() => loginAction(LoginActions.LoginCallback)} />
      </Fragment>);
  }
}

function loginAction(name){
    return (<Login action={name}></Login>);
}

