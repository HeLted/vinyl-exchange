import React, { Component } from "react";
import AddStylesComponent from "./AddStylesComponent";
import { NotificationContext } from "../../../../../../contexts/NotificationContext";
import axios from "axios";
import { Url, Controllers } from "../../../../../../constants/UrlConstants";
import getAntiForgeryAxiosConfig from "../../../../../../functions/getAntiForgeryAxiosConfig";

class AddStylesContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      genreSelectInput: "",
      styleNameInput:""
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = () => {
    const submitObj = {
      name: this.state.styleNameInput,
      genreId: this.state.genreSelectInput
    };

    axios
      .post(
        Url.api + Controllers.styles.name + Url.slash,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification("Successfully added style", 4);
        this.props.functions.handleReLoad();
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to add style!"
        );
      });
  };

  render() {
    return (
      <AddStylesComponent
        data={{
          genreSelectInput: this.state.genreSelectInput,
          styleNameInput: this.state.styleNameInput,
          genres: this.props.data.genres
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default AddStylesContainer;
