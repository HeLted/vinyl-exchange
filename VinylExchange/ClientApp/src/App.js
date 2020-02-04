import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import { MarketplaceContainer } from "./components/marketplace/MarketplaceContainer";
import AddReleaseContainer from "./components/releases/AddReleaseContainer";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;


  handleServerNotification = (notificationMessage ,severty) => {
    console.log("in handleservernotification")
    this.setState({
      currentNotificationMessage: notificationMessage,
      currentNotificationSeverity: severty
    });
  };

  render() {
  
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/marketplace" component={MarketplaceContainer} />
        <AuthorizeRoute path="/releases/addrelease" component={AddReleaseContainer} globalState={this} />
        <Route path="/counter" component={Counter} />
        <Route
          path={ApplicationPaths.ApiAuthorizationPrefix}
          component={ApiAuthorizationRoutes}
        />
      </Layout>
    );
  }
}
