import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { css } from "@emotion/core";
import { Url, Controllers, Queries } from "./../../constants/UrlConstants";
import ShopsTableComponent from "./ShopsTableComponent";
import PulseLoader from "react-spinners/PulseLoader";
import axios from "axios";
import { NotificationContext } from "./../../contexts/NotificationContext";


class ShopsTableContainer extends Component {
  constructor() {
    super();
    this.state = {
      shops: [],
      isLoadMoreShopsLoading: false,
      isThereMoreShopsToLoad: true
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.handleLoadShops();
  }

  componentWillReceiveProps(nextProps) {
    if (
      nextProps.searchValue !== this.props.data.searchValue ){
      this.setState({isThereMoreShopsToLoad:true})
      this.handleLoadShops(true);
    }
  }

  handleLoadShops = shouldUnloadShops => {
    this.setState({
      isLoadMoreShopsLoading: true
    });

    clearTimeout(this.timer);
    this.timer = setTimeout(() => {

      axios
        .get(
          Url.api +
            Controllers.shops.name +
            Controllers.shops.actions.getShops +
            Url.queryStart +
            Queries.searchTerm +
            Url.equal +
            this.props.data.searchValue +
            Url.and +
            Queries.shopsToSkip +
            Url.equal +
            `${shouldUnloadShops ? 0 : this.state.shops.length}` 
        )

        .then(response => {
          if (
            JSON.stringify(this.state.shops) !==
            JSON.stringify(response.data)
          ) {
            return response.data;
          }

          throw "State not updated!!!";
        })
        .then(data => {
          if (data.length === 0) {
            if (shouldUnloadShops) {
              this.setState({
                Shops: [],
                isLoadMoreShopsLoading: false,
                isThereMoreShopsToLoad: false
              });
            } else {
              this.setState({
                isLoadMoreShopsLoading: false,
                isThereMoreShopsToLoad: false
              });
            }
          } else {
            if (this.state.isThereMoreShopsToLoad) {
              this.context.handleAppNotification("Loading more shops", 5);
              this.setState(prevState => {
                const updatedShops = shouldUnloadShops
                  ? []
                  : prevState.shops;
                data.forEach(shop => {
                  updatedShops.push(shop);
                });

                return {
                  shops: updatedShops,
                  isLoadMoreShopsLoading: false,
                  isThereMoreShopsToLoad: true
                };
              });
            }
          }
        })
        .catch(error => {
          console.log(error)
          if (error !== "State not updated!!!") {
            this.context.handleServerNotification(error.response);
          }
          this.setState({ isThereMoreShopsToLoad: false ,isLoadMoreShopsLoading: false });
        });
    }, 1000);
  };

  handleRedirectToShop = shopId => {
    this.props.history.push(`/Shop/${shopId}`, { shopId: shopId });
  };

  render() {
    const renderComponent = this.state.isLoading ? (
      <PulseLoader
        size={15}
        //size={"150px"} this also works
        color={"lime"}
        loading={this.state.isLoading}
      />
    ) : (
      <ShopsTableComponent
        data={{
          shops: this.state.shops,
          isLoadMoreShopsLoading: this.state.isLoadMoreShopsLoading,
          isThereMoreShopsToLoad: this.state.isThereMoreShopsToLoad
        }}
        functions={{
          handleLoadShops: this.handleLoadShops,
          handleRedirectToShop: this.handleRedirectToShop
        }}
      
      />
    );
    return renderComponent;
  }
}

function ShopsTableContainerWrapper(props) {
  return <ShopsTableContainer {...props} />;
}

export default withRouter(ShopsTableContainerWrapper);
