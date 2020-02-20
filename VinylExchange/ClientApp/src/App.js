import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import  MarketplaceContainer  from "./components/marketplace/MarketplaceContainer";
import ReleaseContainer from "./components/releases/ReleaseContainer";
import AddReleaseContainer from "./components/releases/AddReleaseContainer";
import CollectionContainer from "./components/user/collection/CollectionContainer"
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";


import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/marketplace" component={MarketplaceContainer} />
        <AuthorizeRoute path="/releases/addrelease" component={AddReleaseContainer}/>
        <AuthorizeRoute path="/release" component={ReleaseContainer}/>
        <AuthorizeRoute path="/user/collection" component={CollectionContainer}/>
        <Route
          path={ApplicationPaths.ApiAuthorizationPrefix}
          component={ApiAuthorizationRoutes}
        />
      </Layout>
    );
  }
}
