import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { Roles, MultiRoles } from "./constants/RoleConstants";
import MarketplaceContainer from "./components/marketplace/MarketplaceContainer";
import ReleaseContainer from "./components/releases/release/ReleaseContainer";
import AddReleaseContainer from "./components/releases/addRelease/AddReleaseContainer";
import CollectionContainer from "./components/user/collection/CollectionContainer";
import SaleContainer from "./components/sales/sale/SaleContainer";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import RegisterContainer from "./components/api-authorization/authPages/register/RegisterContainer";
import LoginContainer from "./components/api-authorization/authPages/login/LoginContainer";
import EmailConfirmContainer from "./components/api-authorization/authPages/register/EmailConfirmContainer";
import ProfileContainer from "./components/user/profile/ProfileContainer";
import LogoutContainer from "./components/api-authorization/authPages/logout/LogoutContainer";
import LogoutCallbackContainer from "./components/api-authorization/authPages/logout/LogoutCallbackContainer";
import FailedAuthorizationContainer from "./components/api-authorization/authPages/failedAuthorization/FailedAuthorizationContainer";
import AdminPanelContainer from "./components/user/adminPanel/AdminPanelContainer";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/Marketplace" component={MarketplaceContainer} />
        <Route
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
        <Route exact path="/Authentication/Login" component={LoginContainer} />
        <Route
          exact
          path="/Authentication/EmailConfirm"
          component={EmailConfirmContainer}
        />
        <Route
          exact
          path="/Authorization/FailedAuthorization"
          component={FailedAuthorizationContainer}
        />
        <AuthorizeRoute
          path="/Releases/AddRelease"
          component={AddReleaseContainer}
          role={MultiRoles.UserAdmin}
        />
        <AuthorizeRoute
          path="/Sale"
          component={SaleContainer}
          role={MultiRoles.UserAdmin}
        />
        <AuthorizeRoute
          path="/Release"
          component={ReleaseContainer}
          role={MultiRoles.UserAdmin}
        />
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
          role={MultiRoles.UserAdmin}
        />
        <Route
          path={ApplicationPaths.ApiAuthorizationPrefix}
          component={ApiAuthorizationRoutes}
        />
      </Layout>
    );
  }
}
