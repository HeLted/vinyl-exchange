import React, { Component } from "react";
import ConfirmItemRecievedComponent from "./ConfirmItemRecievedComponent";
import hideModal from "./../../../../../../functions/hideModal";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import { Url, Controllers } from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../../../contexts/NotificationContext";

class ConfirmItemRecievedContainer extends Component {
  constructor(props) {
    super(props);
    this.state = { isSubmitLoading: false };
  }

  static contextType = NotificationContext;

  handleOnSubmit = () => {
    this.setState({ isSubmitLoading: true });

    const submitObj = {
      saleId: this.props.data.saleId
    };

    axios
      .put(
        Url.api +
          Controllers.sales.name +
          Controllers.sales.actions.confirmItemRecieved,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isSubmitLoading: false });
        this.context.handleAppNotification(
          "Succesfully marked item as recieved",
          4
        );
        hideModal();
      })
      .catch(error => {
        this.setState({ isSubmitLoading: false });
        this.context.handleServerNotification(error.response, "Failed action!");
      });
  };

  render() {
    return (
      <ConfirmItemRecievedComponent
        functions={{ handleOnSubmit: this.handleOnSubmit }}
        data={{ isSubmitLoading: this.state.isSubmitLoading }}
      />
    );
  }
}

export default ConfirmItemRecievedContainer;
