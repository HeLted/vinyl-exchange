import React from "react";

function ReleaseImagesCarouselComponent(props) {
  const carouselImages = props.data.images.map((image,index) => {
    return (
      <div className={ image.isActive ? "carousel-item active" :"carousel-item" } key={image.id}>
        <img
          className="d-block w-100"
          src={"/file/media" + image.path + image.fileName}
          alt={"slide" + index}
        />
      </div>
    );
  });

  return (
    <div
      id="carouselExampleControls"
      className="carousel slide"
      data-ride="carousel"
      data-interval="false"
  
    >
      <div className="carousel-inner">{carouselImages}</div>
      <a
        className="carousel-control-prev"
        role="button"
        data-slide="prev"
        style={{display: props.data.images.length === 1 ? "none" : "flex"}}
        onClick={props.functions.handleOnPreviousImage}
      >
        <span className="carousel-control-prev-icon" aria-hidden="true"></span>
        <span className="sr-only">Previous</span>
      </a>
      <a
        className="carousel-control-next"
        role="button"
        data-slide="next"
        style={{display: props.data.images.length === 1 ? "none" : "felx"}}
        onClick={props.functions.handleOnNextImage}
      >
        <span className="carousel-control-next-icon" aria-hidden="true"></span>
        <span className="sr-only">Next</span>
      </a>
    </div>
  );
}

export default ReleaseImagesCarouselComponent;
