import PropTypes from 'prop-types';
import DatePicker from 'react-datepicker';
import Button from 'react-bootstrap/Button';
import { forwardRef } from 'react';

const DatePickerButton = ({ date, onDateChange }) => {
    const CustomInput = forwardRef(({ value, onClick }, ref) => (
        <Button className="float-end" onClick={onClick} ref={ref}>
            {value}
        </Button>
    ));
    return (
        <DatePicker
            selected={date}
            onChange={(date) => onDateChange(date)}
            customInput={<CustomInput />}
            wrapperClassName="d-flex"
        />
    );
}

DatePickerButton.defaultProps = {
    date: new Date().getTime()
}

DatePickerButton.propTypes = {
    date: PropTypes.number.isRequired
}

export default DatePickerButton;