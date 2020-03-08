import React, { Component } from "react";
import SaleMenuComponent from "./SaleMenuComponent";
import authService from "./../../../api-authorization/AuthorizeService";

class SaleMenuContainer extends Component {
  constructor() {
    super();
    this.state = {
      currentUserId: "",
    };
  }

  render() {
    return (
      <SaleMenuComponent
        data={{
          sale: this.props.data.sale,
          currentUserId: this.props.data.currentUserId,
          isLoading: this.state.isLoading,
        }}
        functions={{
                handleReLoadSale: this.props.functions.handleReLoadSale
              }}
      />
    );
  }
}

export default SaleMenuContainer;
