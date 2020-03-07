import React, { Component } from "react";
import SaleComponent from "./SaleComponent";
import axios from "axios";
import { Url, Controllers, Queries } from "./../../../constants/UrlConstants";
import { NotificationContext } from "./../../../contexts/NotificationContext";
import authService from "./../../../components/api-authorization/AuthorizeService";

class SaleContainer extends Component {
  static contextType = NotificationContext;

  constructor() {
    super();
    this.state = {
      sale: {
        id: "",
        status: 0,
        sellerId: "",
        buyerId: "",
        sellerId: "",
        releaseId: "",
        buyerUsername: "",
        sellerUsername: "",
        vinylGrade: 0,
        sleeveGrade: 0,
        price: 0,
        description: "",
        statusText: "",
        shipsFrom: "",
        shipsTo: "",
        shippingPrice: 0
      },
      isLoading: true,
      isChatShown: false
    };
  }

  componentDidMount() {
    this.loadSale();
  }

  handleReLoadSale = () => {
    this.loadSale();
  };

  loadSale = () => {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.sales.name +
          Url.slash +
          this.props.location.pathname.replace("/Sale/", "")
      )
      .then(response => {
        const data = response.data;
        this.context.handleAppNotification("Loaded sale data", 5);
        this.setState({
          sale: {
            id: data.id,
            status: data.status,
            sellerId: data.sellerId,
            buyerId: data.buyerId,
            sellerUsername: data.sellerUsername,
            buyerUsername: data.buyerUsername,
            releaseId: data.releaseId,
            vinylGrade: data.vinylGrade,
            sleeveGrade: data.sleeveGrade,
            price: data.price,
            description: data.description,
            statusText: data.statusText,
            vinylConditionText: data.vinylConditionText,
            sleeveConditionText: data.sleeveConditionText,
            shipsFrom: data.shipsFrom,
            shipsTo: data.ShipsTo,
            shippingPrice: data.shippingPrice
          },
          isLoading: false
        });
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(error.response);
      });
  };

  handleToggleChat = () => {
    this.setState(prevState => {
      return { isChatShown: prevState.isChatShown ? false : true };
    });
  };

  render() {
    return (
      <SaleComponent
        data={{
          sale: this.state.sale,
          isLoading: this.state.isLoading,
          isChatShown: this.state.isChatShown
        }}
        functions={{
          handleReLoadSale: this.handleReLoadSale,
          handleToggleChat: this.handleToggleChat
        }}
      />
    );
  }
}

export default SaleContainer;
