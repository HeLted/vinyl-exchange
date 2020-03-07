import React, { Component } from "react";
import PlaceOrderModalComponent from "./PlaceOrderModalComponent";
import { Url, Controllers } from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import $ from "jquery";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import hideModal from "./../../../../../../functions/hideModal";


class PlaceOrderModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userAddresses: [],
      isLoading: false,
      isSubmitLoading: false,
      addressSelectInput: ""
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.addresses.name +
          Controllers.addresses.actions.getUserAddresses
      )
      .then(response => {
        this.setState({
          userAddresses: response.data.map(addressObj => {
            return {
              id: addressObj.id,
              name: `${addressObj.country}-${addressObj.town}-${addressObj.postalCode}-${addressObj.fullAddress}`
            };
          }),
          isLoading: false
        });
        this.context.handleAppNotification("Loaded user addresses", 5);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to load user addresses!"
        );
      });
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleFlushModal = () => {
    hideModal();
  };

  handleOnSubmit = () => {
    this.setState({ isSubmitLoading: true });

    const submitObj = {
      saleId: this.props.data.saleId,
      addressId: this.state.addressSelectInput
    };

    axios
      .put(
        Url.api + Controllers.sales.name + Controllers.sales.actions.placeOrder,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isSubmitLoading: false });
        this.context.handleAppNotification("Succesfully placed order", 4);
        hideModal();
        this.props.functions. handleReLoadSale();
      })
      .catch(error => {
        this.setState({ isSubmitLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to place order!"
        );
      });
  };

  render() {
    return (
      <PlaceOrderModalComponent
        data={{
          userAddresses: this.state.userAddresses,
          isLoading: this.state.isLoading,
          addressSelectInput: this.state.addressSelectInput
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleFlushModal: this.handleFlushModal,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default PlaceOrderModalContainer;
