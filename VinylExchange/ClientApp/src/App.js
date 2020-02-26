import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import  MarketplaceContainer  from "./components/marketplace/MarketplaceContainer";
import ReleaseContainer from "./components/releases/release/ReleaseContainer";
import AddReleaseContainer from "./components/releases/addRelease/AddReleaseContainer";
import CollectionContainer from "./components/user/collection/CollectionContainer"
import ShopsContainer from "./components/shops/ShopsContainer";
import AddShopContainer from "./components/shops/addShop/AddShopContainer"
import SaleContainer from "./components/sales/sale/SaleContainer"
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";
import RegisterContainer from "./components/api-authorization/authPages/register/RegisterContainer"
import LoginContainer from "./components/api-authorization/authPages/login/LoginContainer"


import "./custom.css";


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/Marketplace" component={MarketplaceContainer} />
        <Route exact path="/Shops" component={ShopsContainer} />
        <Route exact path="/Authentication/Register" component={RegisterContainer}/>
        <Route exact path="/Authentication/Login" component={LoginContainer}/>
        <AuthorizeRoute path="/Releases/AddRelease" component={AddReleaseContainer}/>
        <AuthorizeRoute  path="/Shops/AddShop" component={AddShopContainer}/>
        <AuthorizeRoute  path="/Sale" component={SaleContainer}/>
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
