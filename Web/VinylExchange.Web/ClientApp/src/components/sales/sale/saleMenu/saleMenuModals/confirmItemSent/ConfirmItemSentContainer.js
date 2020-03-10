import React, { Component } from "react";
import ConfirmItemSentComponent from "./ConfirmItemSentComponent";
import hideModal from "./../../../../../../functions/hideModal";
import getAntiForgeryAxiosConfig from "./../../../../../../functions/getAntiForgeryAxiosConfig";
import {Url,Controllers} from "./../../../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../../../contexts/NotificationContext";


class ConfirmItemSentContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {isSubmitLoading:false};
  }

  static contextType = NotificationContext;

  handleOnSubmit = () => {
    this.setState({ isSubmitLoading: true });

    const submitObj = {
      saleId: this.props.data.saleId
    };

    axios
      .put(
        Url.api + Controllers.sales.name + Controllers.sales.actions.confirmItemSent,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isSubmitLoading: false });
        this.context.handleAppNotification(
          "Succesfully marked item as sent",
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
      <ConfirmItemSentComponent
        functions={{ handleOnSubmit: this.handleOnSubmit }}
        data={{isSubmitLoading:this.state.isSubmitLoading}}
      />
    );
  }
}

export default ConfirmItemSentContainer;
