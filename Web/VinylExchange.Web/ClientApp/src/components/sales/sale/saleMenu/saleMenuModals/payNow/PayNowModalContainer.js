import React, { Component } from "react";
import PayNowModalComponent from "./PayNowModalComponent";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import {
  Url,
  Controllers
} from "./../../.../../../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import $ from "jquery"

class PayNowModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  handleApprovePayment = orderId => {
    const submitObj = {
      saleId: this.props.data.saleId,
      orderId: orderId
    };

    this.setState({ isLoading: true });

    axios
      .put(
        Url.api +
          Controllers.sales.name +
          Controllers.sales.actions.completePayment,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        $(".modal-backdrop").hide();
        $(".modal").hide();
        this.setState({ isLoading: false });
        this.context.handleAppNotification("Successfull payment", 4);
        this.props.functions.handleReLoadSale();
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Payment failed!"
        );
      });
  };

  render() {
    return (
      <PayNowModalComponent
        data={{
          price: this.props.data.price,
          shippingPrice: this.props.data.shippingPrice,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleApprovePayment: this.handleApprovePayment
        }}
      />
    );
  }
}

export default PayNowModalContainer;
