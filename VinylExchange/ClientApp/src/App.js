import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import  MarketplaceContainer  from "./components/marketplace/MarketplaceContainer";
import ReleaseContainer from "./components/releases/release/ReleaseContainer";
import AddReleaseContainer from "./components/releases/addRelease/AddReleaseContainer";
import CollectionContainer from "./components/releases/release/ReleaseContainer"
import ShopsContainer from "./components/shops/ShopsContainer";
import AddShopContainer from "./components/shops/addShop/AddShopContainer"
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
        <Route path="/Marketplace" component={MarketplaceContainer} />
        <Route exact path="/Shops" component={ShopsContainer} />
        <AuthorizeRoute path="/Releases/AddRelease" component={AddReleaseContainer}/>
        <AuthorizeRoute  path="/Shops/AddShop" component={AddShopContainer}/>
        <AuthorizeRoute path="/Release" component={ReleaseContainer}/>
        <AuthorizeRoute path="/User/Collection" component={CollectionContainer}/>
        <Route
          path={ApplicationPaths.ApiAuthorizationPrefix}
          component={ApiAuthorizationRoutes}
        />
      </Layout>
    );
  }
}
