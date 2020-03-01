import React, { Component } from "react";
import AddressesTableComponent from "./AddressesTableComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";

class AddressesTableContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      addresses: [],
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.loadAddresses();
  }

  loadAddresses = () => {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.addresses.name +
          Controllers.addresses.actions.getUserAddresses
      )
      .then(response => {
        this.setState({
          addresses: response.data,
          isLoading: false
        });
        this.context.handleAppNotification("Loaded user addresses");
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to load user addresses!"
        );
      });
  };

  

  handleDeleteAddress = addressId => {
    this.setState({ isLoading: true });
    axios
      .delete(
        Url.api + Controllers.addresses.name + Url.slash + addressId,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.loadAddresses();
        this.context.handleAppNotification("Successfully deleted address", 4);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to delete address!"
        );
      });
  };

  render() {
    return (
      <AddressesTableComponent
        data={{
          addresses: this.state.addresses,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleDeleteAddress: this.handleDeleteAddress
         
        }}
      />
    );
  }
}

export default AddressesTableContainer;
