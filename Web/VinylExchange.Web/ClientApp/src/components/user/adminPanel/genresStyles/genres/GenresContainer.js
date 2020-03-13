import React, { Component } from "react";
import GenresComponent from "./GenresComponent";
import axios from "axios";
import {
  Url,
  Controller,
  Controllers
} from "../../../../../constants/UrlConstants";
import { NotificationContext } from "../../../../../contexts/NotificationContext";

class GenresContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      genres: []
    };
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

  handleAddGenre = genreObj => {
    this.setState(prevState => {
      const updatedGenres = prevState.genres.slice();
      updatedGenres.push(genreObj);

      return { genres: updatedGenres };
    });
  };

  handleRemoveGenre = genreId => {
 
    // this.setState(prevState => {
    //   const updatedGenres = prevState.genres.filter(genreObj => {
    //     return genreObj.id !== genreId;
    //   });

    //   return { genres: updatedGenres };
    // });
  };

  render() {
    return (
      <GenresComponent
        data={{ genres: this.state.genres }}
        functions={{
          handleReLoad:this.props.functions.handleReLoad
        }}
      />
    );
  }
}

export default GenresContainer;
