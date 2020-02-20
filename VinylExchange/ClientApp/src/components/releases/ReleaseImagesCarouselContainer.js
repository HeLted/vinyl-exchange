import React, { Component } from "react";
import ReleaseImagesCarouselComponent from "./ReleaseImagesCarouselComponent";
import axios from "axios";
import { Url, Controllers, Queries } from "./../../constants/UrlConstants";

class ReleaseImagesCarouselContainer extends Component {
  constructor() {
    super();
    this.state = {
      selectedImage: null,
      images: []
    };
  }

  componentDidMount() {
    const self = this;
    axios
      .get(
        Url.api +
          Controllers.releaseImages.name +
          Controllers.releaseImages.actions.getAllImagesForRelease +
          Url.queryStart +
          Queries.releaseId +
          Url.equal +
          this.props.data.releaseId
      )
      .then(function(response) {
        const releaseImages = response.data;

        self.setState({
          images: releaseImages.map((image, index) => {
            index === 0 ? (image.isActive = true) : (image.isActive = false);
            return image;
          })
        });
      })
      .catch(function() {});
  }

  handleOnNextImage = () => {
    this.setState(prevState => {
      let currentActiveImageFound = false;
      const images = prevState.images.slice();

      for (let i = 0; i < prevState.images.length; i++) {
        if (currentActiveImageFound) {
          currentActiveImageFound = false;
          images[i].isActive = true;
        } else if (images[i].isActive) {
          currentActiveImageFound = true;
          images[i].isActive = false;
        }
      }

      if (currentActiveImageFound) {
        currentActiveImageFound = false;
        images[0].isActive = true;
      }

      return { images };
    });
  };

  handleOnPreviousImage = () => {
    this.setState(prevState => {
      let currentActiveImageFound = false;
      const images = prevState.images.slice();

      for (let i = prevState.images.length - 1; i >= 0; i--) {
        if (currentActiveImageFound) {
          currentActiveImageFound = false;
          images[i].isActive = true;
        } else if (images[i].isActive) {
          currentActiveImageFound = true;
          images[i].isActive = false;
        }
      }

      if (currentActiveImageFound) {
        currentActiveImageFound = false;
        images[prevState.images.length - 1].isActive = true;
      }

      return { images };
    });
  };

  render() {
    return (
      <ReleaseImagesCarouselComponent
        data={{ images: this.state.images }}
        functions={{
          handleOnNextImage: this.handleOnNextImage,
          handleOnPreviousImage: this.handleOnPreviousImage
        }}
      />
    );
  }
}

export default ReleaseImagesCarouselContainer;
