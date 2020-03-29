import React, { Component } from "react";
import { Route, Switch } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/home/Home";
import { Roles, MultiRoles } from "./constants/RoleConstants";
import MarketplaceContainer from "./components/marketplace/MarketplaceContainer";
import ReleaseContainer from "./components/releases/release/ReleaseContainer";
import AddReleaseContainer from "./components/releases/addRelease/AddReleaseContainer";
import CollectionContainer from "./components/user/collection/CollectionContainer";
import SaleContainer from "./components/sales/sale/SaleContainer";
import AuthorizeRoute from "./components/api-authorization/routeSecurityLevel/AuthorizeRoute";
import AnonymousRoute from "./components/api-authorization/routeSecurityLevel/AnonymousRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import RegisterContainer from "./components/api-authorization/authPages/register/RegisterContainer";
import LoginContainer from "./components/api-authorization/authPages/login/LoginContainer";
import EmailConfirmContainer from "./components/api-authorization/authPages/emailConfirm/EmailConfirmContainer";
import ProfileContainer from "./components/user/profile/ProfileContainer";
import LogoutContainer from "./components/api-authorization/authPages/logout/LogoutContainer";
import LogoutCallbackContainer from "./components/api-authorization/authPages/logout/LogoutCallbackContainer";
import FailedAuthorizationContainer from "./components/api-authorization/authPages/failedAuthorization/FailedAuthorizationContainer";
import AdminPanelContainer from "./components/user/adminPanel/AdminPanelContainer";
import PageNotFoundContainer from "./components/common/pages/pageNotFound/PageNotFoundContainer";
import ServerErrorContainer from "./components/common/pages/serverError/ServerErrorContainer";
import ChangeEmailContainer from "./components/api-authorization/authPages/changeEmail/ChangeEmailContainer";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Switch>
          <Route exact path="/" component={Home} />
          <Route exact path="/Marketplace" component={MarketplaceContainer} />
          <AnonymousRoute
            exact
            path="/Authentication/Register"
            component={RegisterContainer}
          />
          <Route
            exact
            path="/Authentication/Logout"
            component={LogoutContainer}
          />
          <Route
            exact
            path="/Authentication/Logout-Callback"
            component={LogoutCallbackContainer}
          />
          <AnonymousRoute
            exact
            path="/Authentication/Login"
            component={LoginContainer}
          />
          <AuthorizeRoute
            exact
            path="/Authentication/EmailConfirm"
            component={EmailConfirmContainer}
          />
          <AuthorizeRoute
            exact
            path="/Authentication/ChangeEmail"
            component={ChangeEmailContainer}
          />
          <Route
            exact
            path="/Error/FailedAuthorization"
            component={FailedAuthorizationContainer}
          />
          <Route
            exact
            path="/Error/ServerError"
            component={ServerErrorContainer}
          />
          <AuthorizeRoute
            exact
            path="/Releases/AddRelease"
            component={AddReleaseContainer}
            role={MultiRoles.UserAdmin}
          />
          <AuthorizeRoute
            exact
            path="/Sales"
            component={SaleContainer}
            role={MultiRoles.UserAdmin}
          />
          <Route path="/Releases" component={ReleaseContainer} />
          <AuthorizeRoute
            exact
            path="/User/Collection"
            component={CollectionContainer}
            role={MultiRoles.UserAdmin}
          />
          <AuthorizeRoute
            exact
            path="/User/Profile"
            component={ProfileContainer}
            role={MultiRoles.UserAdmin}
          />
          <AuthorizeRoute
            exact
            path="/User/AdminPanel"
            component={AdminPanelContainer}
            role={Roles.Admin}
          />
          <Route
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={ApiAuthorizationRoutes}
          />
          <Route component={PageNotFoundContainer} /> // without path
        </Switch>
      </Layout>
    );
  }
}
