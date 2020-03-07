import React, { Fragment } from "react";
import TextInput from "./../../../common/inputComponents/TextInput";
import UserThumbnail from "./../../../common/UserThumbnail";
import uuid4 from "./../../../../functions/guidGenerator";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function SaleChatComponent(props) {
  const listItems = props.data.messages.map(messageObj => {
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
      <div className="input-group">
        <TextInput
          value={props.data.messageInput}
          id="messageInput"
          onChange={props.functions.handleOnChange}
        />
        <button
          className="btn btn-primary"
          onClick={props.functions.handleSendMessage}
        >
          Send
        </button>
      </div>
    </div>
  );
}

export default SaleChatComponent;
