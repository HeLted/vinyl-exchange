import React, { Component } from "react";
import AddGenresComponent from "./AddGenresComponent";
import { NotificationContext } from "../../../../../../contexts/NotificationContext";
import axios from "axios";
import { Url, Controllers } from "../../../../../../constants/UrlConstants";
import getAntiForgeryAxiosConfig from "../../../../../../functions/getAntiForgeryAxiosConfig";

class AddGenresContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      genreNameInput: ""
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = () => {
   const submitObj = {
      name: this.state.genreNameInput
    };

    axios.post(
      Url.api + Controllers.genres.name + Url.slash,
      submitObj,
      getAntiForgeryAxiosConfig()
    ).then(response=>{
      this.context.handleAppNotification("Successfully added genre",4)
      this.props.functions.handleReLoad();
    }).catch(error=>{
      this.context.handleServerNotification(error.response,"Failed to add genre!")
    });
  };

  render() {
    return (
      <AddGenresComponent
        data={{ genreNameInput: this.state.genreNameInput }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default AddGenresContainer;
