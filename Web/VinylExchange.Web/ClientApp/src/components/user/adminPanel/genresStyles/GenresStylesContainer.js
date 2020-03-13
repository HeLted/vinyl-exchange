import React, { Component } from "react";
import GenresStylesComponent from "./GenresStylesComponent";

class GenresStylesContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      key: Math.random()
    };
  }

  handleReLoad = () => {
    this.setState({ key: Math.random() });
  };

  render() {
    return (
      <GenresStylesComponent
        key={this.state.key}
        functions={{ handleReLoad: this.handleReLoad }}
      />
    );
  }
}

export default GenresStylesContainer;
