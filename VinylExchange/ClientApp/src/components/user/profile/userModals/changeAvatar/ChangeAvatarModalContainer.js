import React, { Component } from "react";
import ChangeAvatarModalComponent from "./ChangeAvatarModalComponent";
import getAntiForgeryAxiosConfig from "./../../../../../functions/getAntiForgeryAxiosConfig";
import { Url, Controllers } from "./../../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../../contexts/NotificationContext";
import $ from "jquery";

class ChangeAvatarModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      avatarInput: null,
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  handleOnFileUpload = e => {
    let file = e.target.files[0];
    this.setState({
      [e.target.id]: file
    });
  };

  handleOnSubmit = () => {
    this.setState({ isLoading: true });

    const fileFormData = new FormData();
    fileFormData.append("avatar", this.state.avatarInput);

    axios
      .put(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.changeUserAvatar,
        fileFormData,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        $(".modal").hide();
        $(".modal-backdrop").hide();

        this.setState({ isLoading: false });
        this.context.handleAppNotification(
          "Succesfully changed your avatar",
          4
        );
        this.props.functions.handleShouldAvatarUpdate();
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to change your avatar!"
        );
      });
  };

  render() {
    return (
      <ChangeAvatarModalComponent
        functions={{
          handleOnFileUpload: this.handleOnFileUpload,
          handleOnSubmit: this.handleOnSubmit
        }}
        data={{ isLoading: this.state.isLoading }}
      />
    );
  }
}

export default ChangeAvatarModalContainer;
