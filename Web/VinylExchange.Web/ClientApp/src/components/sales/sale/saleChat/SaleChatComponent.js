import React, { Fragment } from "react";
import TextInput from "./../../../common/inputComponents/TextInput";
import UserThumbnail from "./../../../common/UserThumbnail";
import uuid4 from "./../../../../functions/guidGenerator";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

function SaleChatComponent(props) {
  const listItems = props.data.messages.map((messageObj) => {
    return (
      <li key={uuid4()}>
        <div
          className="alert alert-primary round-border text-left"
          role="alert"
          style={{ backgroundColor: "#0084ff" }}
        >
          <h6 style={{ color: "white", wordWrap: "break-word" }}>
            <UserThumbnail
              data={{ avatar: messageObj.avatar, height: 30, width: 30 }}
            />{" "}
            : {messageObj.message}{" "}
          </h6>
        </div>
      </li>
    );
  });

  return props.data.isLoading ? (
    <BorderSpinner />
  ) : (
    <div className="chat-container border">
      <ul className="chat border ">{listItems}</ul>
      <br />

      <div className="form-group">
        <div className="row m-0">
          <div className="col-lg-9 col-md-9 col-sm-9 col-xs-12 p-0">
            <TextInput
              value={props.data.messageInput}
              id="messageInput"
              onChange={props.functions.handleOnChange}
            />
          </div>

          <div className="col-lg-3 col-md-3 col-sm-3 col-xs-12 text-center p-0">
            <button
              className="btn btn-primary w-100"
              onClick={props.functions.handleSendMessage}
            >
              <FontAwesomeIcon icon={faArrowRight} />
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SaleChatComponent;
