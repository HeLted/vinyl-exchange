import React, { Component } from "react";
import StylesComponent from "./StylesComponent";
import axios from "axios";
import {
  Url,
  Controller,
  Controllers
} from "../../../../../constants/UrlConstants";
import { NotificationContext } from "../../../../../contexts/NotificationContext";

class StylesContainer extends Component {
  constructor(props) {
    super(props);
    this.state = { genres: [] };
  }

  static contextType = NotificationContext;

  componentWillMount() {
    this.loadGenres();
  }

  loadGenres = () => {
    axios
      .get(Url.api + Controllers.genres.name + Url.slash)
      .then(response => {
        this.context.handleAppNotification("Loaded genres", 5);
        this.setState({ genres: response.data });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load genres!"
        );
      });
  };
  render() {
    return (
      <StylesComponent
        data={{ genres: this.state.genres }}
        functions={{ handleReLoad: this.props.functions.handleReLoad }}
      />
    );
  }
}

export default StylesContainer;
