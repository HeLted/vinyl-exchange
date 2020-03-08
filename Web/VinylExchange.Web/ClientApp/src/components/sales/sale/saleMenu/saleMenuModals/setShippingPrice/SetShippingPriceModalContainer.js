import React, { Component } from "react";
import SetShippingPriceModalComponent from "./SetShippingPriceModalComponent";
import { Url, Controllers } from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import hideModal from "../../../../../../functions/hideModal";

class SetShippingPriceModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      priceInput: 0,
      isLoading: false
    };
  }

  static contextType = NotificationContext

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = () => {
    this.setState({ isLoading: true });

    const submitObj = {
      saleId: this.props.data.saleId,
      shippingPrice: this.state.priceInput
    };

    axios
      .put(
        Url.api +
          Controllers.sales.name +
          Controllers.sales.actions.setShippingPrice,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification("Succesfully set shiping price", 4);
        hideModal();
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to set shipping price!"
        );
      });
  };

  render() {
    return (
      <SetShippingPriceModalComponent
        data={{
          priceInput: this.state.priceInput,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default SetShippingPriceModalContainer;
