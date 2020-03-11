import React, { Component } from "react";
import RemoveGenresComponent from "./RemoveGenresComponent";
import axios from "axios";
import {
  Url,
  Controller,
  Controllers
} from "./../../../../constants/UrlConstants";
import {NotificationContext} from "./../../../../contexts/NotificationContext";

class RemoveGenresContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      genres: [],
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.genres.name +
           Url.slash
      )
      .then(response => {
        this.context.handleAppNotification("Loaded genres", 5);
        this.setState({genres:response.data, isLoading: false });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load genres!"
        );
        this.setState({ isLoading: false });
      });
  }

  handleOnDeleteGenre = genreId => {
    console.log(genreId);
  }

  render() {
    return (
      <RemoveGenresComponent
        data={{ genres: this.state.genres, isLoading: this.state.isLoading }}
        functions={{ handleOnDeleteGenre:this.handleOnDeleteGenre}}
      />
    );
  }
}

export default RemoveGenresContainer;
