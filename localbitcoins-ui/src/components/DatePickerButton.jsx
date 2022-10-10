import PropTypes from 'prop-types';
import DatePicker from 'react-datepicker';
import Button from 'react-bootstrap/Button';
import { forwardRef } from 'react';

const DatePickerButton = ({ date, onDateChange, dateFormat, showMonthYearPicker, showFullMonthYearPicker }) => {
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
            dateFormat={dateFormat}
            showMonthYearPicker={showMonthYearPicker}
            showFullMonthYearPicker={showFullMonthYearPicker}
        />
    );
}

DatePickerButton.defaultProps = {
    date: new Date().getTime(),
    dateFormat: 'MM/dd/yyyy',
    showMonthYearPicker: false,
    showFullMonthYearPicker: false
}

DatePickerButton.propTypes = {
    date: PropTypes.number.isRequired,
    dateFormat: PropTypes.string,
    showMonthYearPicker: PropTypes.bool,
    showFullMonthYearPicker: PropTypes.bool
}

export default DatePickerButton;