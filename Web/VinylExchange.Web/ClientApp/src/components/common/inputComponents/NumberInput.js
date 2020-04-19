import React, { Component } from "react";
import InputValidationMessage from "./../clientSideValidation/InputValidationMessage";
import uuid4 from "./../../../functions/guidGenerator";

const NumericRegex = new RegExp("^[0-9]*$");
const MoneyRegex = new RegExp("^[0-9.]*");

class NumberInput extends Component {
  constructor(props) {
    super(props);
    this.state = {
      validationRules: {
        isRequired: false,
        isMoney: false,
        minNumber: 0,
        maxNumber: 1000000
      },
      validationMessages: []
    };
  }

  componentDidMount() {
    if (this.props.required) {
      this.setState({
        validationRules: {
          isRequired: true
        }
      });
    }

    if (this.props.money) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            isMoney: true
          }
        };
      });
    }

    if (this.props.minNumber != undefined) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            minNumber: this.props.minNumber
          }
        };
      });
    }

    if (this.props.maxNumber != undefined) {
      this.setState(prevState => {
        return {
          validationRules: {
            ...prevState.validationRules,
            maxNumber: this.props.maxNumber
          }
        };
      });
    }
  }

  onChange = event => {
    this.props.onChange(event);
    this.InputValidation(event.target.value);
  };

  InputValidation = value => {
    const validationRules = this.state.validationRules;

    const updatedValidationMessages = [];

    if (validationRules.isRequired) {
      if (value.length === 0) {
        updatedValidationMessages.push("This field is required!");
      }
    }

    if (
      value < validationRules.minNumber ||
      value > validationRules.maxNumber
    ) {
      updatedValidationMessages.push(
        `Field must be in range between ${validationRules.minNumber} and ${validationRules.maxNumber}!`
      );
    }

    if (validationRules.isMoney) {
      if (!MoneyRegex.test(value)) {
        updatedValidationMessages.push(
          "Special characters are not allowed in money input!"
        );
      }
    } else if (!NumericRegex.test(value)) {
      updatedValidationMessages.push(
        "Field must contain only numeric (integer) characters!"
      );
    }

    this.setState({ validationMessages: updatedValidationMessages });
  };
  render() {
    const extraClasses =
      this.props.extraClasses === undefined
        ? ""
        : " " + this.props.extraClasses;

    const validationMessages = this.state.validationMessages.map(
      validationMsg => {
        return <InputValidationMessage message={validationMsg} key={uuid4()} />;
      }
    );

    return (
      <div className="text-left">
        <input
          type="number"
          className={"form-control" + extraClasses}
          id={this.props.id}
          value={this.props.value}
          name={this.props.id}
          onFocus={event => {
            this.InputValidation(event.target.value);
          }}
          onChange={this.onChange}
          placeholder={this.props.placeholder}
        />
        {validationMessages}
      </div>
    );
  }
}

export default NumberInput;
