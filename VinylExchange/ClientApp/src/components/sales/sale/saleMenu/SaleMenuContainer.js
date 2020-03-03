import React, { Component } from "react";
import SaleMenuComponent from "./SaleMenuComponent";
import authService from "./../../../api-authorization/AuthorizeService";

class SaleMenuContainer extends Component {
  constructor() {
    super();
    this.state = {
      currentUserId: "",
      isLoading: true
    };
  }

  componentDidMount() {
    this.setState({ isLoading: true });

    authService.getUser().then(userObj => {
      this.setState({ currentUserId: userObj.sub, isLoading: false });
    });
  }

  render() {
    return (
      <SaleMenuComponent
        data={{
          sale: this.props.data.sale,
          currentUserId: this.state.currentUserId,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleReLoadSale: this.props.functions.handleReLoadSale
        }}
      />
    );
  }
}

export default SaleMenuContainer;
