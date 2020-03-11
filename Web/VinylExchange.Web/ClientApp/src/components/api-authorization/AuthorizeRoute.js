import React from "react";
import { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import {
  ApplicationPaths,
  QueryParameterNames
} from "./ApiAuthorizationConstants";
import authService from "./AuthorizeService";
import { Roles } from "./../../constants/RoleConstants";
import PageSpinner from "./../common/spinners/PageSpinner";

export default class AuthorizeRoute extends Component {
  constructor(props) {
    super(props);

    this.state = {
      ready: false,
      authenticated: false,
      isAuthorized: false,
      isAuthLoading:true
    };
  }

  componentDidMount() {
    this.setState({isAuthLoading:true})
    this._subscription = authService.subscribe(() =>
      this.authenticationChanged()
    );
    this.populateAuthenticationState();

    authService.getUser().then(userObj => {
      const requiredRole =
        this.props.role != undefined ? this.props.role : Roles.User;
     
      if (userObj !== null) {
        if (requiredRole.includes(userObj.role)) {
          this.setState({ isAuthorized: true,isAuthLoading:false });
        } else {
          this.setState({ isAuthorized: false,isAuthLoading:false });
        }
      }

      this.setState({isAuthLoading:false});
    });
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  render() {
    const { ready, authenticated, isAuthorized ,isAuthLoading} = this.state;
    let redirectUrl = `${ApplicationPaths.Login}?${
      QueryParameterNames.ReturnUrl
    }=${encodeURI(window.location.href)}`;
      
    !isAuthorized && authenticated && (redirectUrl = "/Authorization/FailedAuthorization");
    if (!ready || isAuthLoading) {
      return (
        <div>
          <PageSpinner />
        </div>
      );
    } else {
      const { component: Component, ...rest } = this.props;
      return (
        <Route
          {...rest}
          render={props => {
            if (authenticated && isAuthorized) {
              return <Component {...props} />;
            } else {
              return <Redirect to={redirectUrl} />;
            }
          }}
        />
      );
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ ready: true, authenticated });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}
