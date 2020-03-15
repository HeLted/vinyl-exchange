import React, { Component } from "react";
import RemoveSaleModalComponent from "./RemoveSaleModalComponent";
import hideModal from "./../../../../../../functions/hideModal";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import { Url, Controllers } from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../../../contexts/NotificationContext";
import {withRouter} from "react-router-dom";

class RemoveSaleModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = { isSubmitLoading: false };
  }

  static contextType = NotificationContext;

  handleOnSubmit = () => {
    this.setState({ isSubmitLoading: true });

    axios
      .delete(
        Url.api + Controllers.sales.name + Url.slash+this.props.data.saleId,

        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isSubmitLoading: false });
        this.context.handleAppNotification("Succesfully deleted sale", 4);
        hideModal();
        this.props.history.push("/Marketplace");
      })
      .catch(error => {
        this.setState({ isSubmitLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to delete sale!"
        );
      });
  };

  render() {
    return (
      <RemoveSaleModalComponent
        data={{ isSubmitLoading: this.state.isSubmitLoading }}
        functions={{ handleOnSubmit: this.handleOnSubmit }}
      />
    );
  }
}

export default withRouter(RemoveSaleModalContainer);
