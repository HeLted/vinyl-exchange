import React from "react";

export default function ServerNotification(props) {
  let alertTypeClass = "alert-success";

  if (props.severity === 3) {
    alertTypeClass = "alert-success";
  } else if (props.severity === 2) {
    alertTypeClass = "alert-primary";
  } else if (props.severity === 1) {
    alertTypeClass = "alert-danger";
  }

  return (
    <div className={"alert " + alertTypeClass} role="alert">
      {props.message}
    </div>
  );
}
