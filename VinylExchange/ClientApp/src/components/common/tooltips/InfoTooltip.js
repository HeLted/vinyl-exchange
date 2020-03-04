import React,{Component} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faInfo } from "@fortawesome/free-solid-svg-icons";

class InfoTooltip extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    return (
      <span
        className="d-inline-block"
        tabIndex="0"
        data-toggle="tooltip"
        title={this.props.data.tooltipValue}
      >
        <button
          className="btn btn-primary btn-lg"
          style={{pointerEvents: "none"}}
          type="button"
          disabled
        >
          <FontAwesomeIcon icon={faInfo} />
        </button>
      </span>
    );
  }
}

export default InfoTooltip;
