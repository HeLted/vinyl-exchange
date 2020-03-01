import React, { Component } from "react";
import AddAddressComponent from "./AddAddressComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";

class AddAddressContainer extends Component {
  constructor() {
    super();
    this.state = {
      countryInput: "",
      townInput: "",
      postalCodeInput: "",
      fullAddressInput: "",
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();
    event.stopPropagation();

    const submitFormObj = {
      country: this.state.countryInput,
      town: this.state.townInput,
      postalCode: this.state.postalCodeInput,
      fullAddress: this.state.fullAddressInput
    };

    this.setState({ isLoading: true });

    axios
      .post(
        Url.api + Controllers.addresses.name + Url.slash,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification("Succesfully added address", 4);
        this.props.functions.handleOnToggleModalPage();
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "There was An error when creating address!"
        );
      });
  };

  render() {
    return (
      <AddAddressComponent
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
        data={{
          countryInput: this.state.countryInput,
          townInput: this.state.townInput,
          postalCodeInput: this.state.postalCodeInput,
          fullAddressInput: this.state.fullAddressInput,
          isLoading : this.state.isLoading
        }}
      />
    );
  }
}

export default AddAddressContainer;
