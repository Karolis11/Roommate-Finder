import { useFormik } from 'formik';
import { Select, MenuItem } from '@mui/material';
import axios from 'axios';

export const FilterComponent = (props) => {

    const getListings = (values, city) => {
        axios({
            method: 'get',
            url: `https://localhost:44332/listing/sort?sort=${values.sort}&city=${encodeURIComponent(city)}`,
        })
        .then((response) => {
            props.updateListings(response.data);             
        })
    }

    const formik = useFormik({
        initialValues: {
            sort: 0,
        },
        onSubmit: (values) => {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition((position) => {
                    axios({
                        method: "get",
                        url: `https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${position.coords.latitude}&longitude=${position.coords.longitude}&localityLanguage=default`
                    })
                    .then((response) => {
                        getListings(values, response.data.city);
                    })
                });
            } else {
                getListings(values, null);
            } 
        }
    });

    return (
        <div className="filter-top-container">
            <label htmlFor="sort">Sort By</label>
            <Select
                name="sort"
                onChange={(e) => {formik.handleChange(e); formik.submitForm(formik.values);}}
                value={formik.values.sort}
            >
                <MenuItem value={0}>Maximum price</MenuItem>
                <MenuItem value={1}>Number of roommates</MenuItem>
                <MenuItem value={2}>City</MenuItem>
            </Select>
        </div>
    );
}