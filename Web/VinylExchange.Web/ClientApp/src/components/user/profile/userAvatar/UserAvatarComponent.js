import React from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function UserAvatarComponent(props) {
  const component = props.data.isLoading ? (
    <BorderSpinner />
  ) : (
    <img
      className="img-thumbnail image-responsive"
      src={"data:image/png;base64, " + props.data.avatar}
     
    ></img>
  );

  return component;
}

export default UserAvatarComponent;
