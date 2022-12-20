import { useFormik } from 'formik';
import { useState } from 'react';
import { Select, MenuItem, Slider } from '@mui/material';
import axios from 'axios';
import {DropdownCheckboxList} from './DropdownCheckboxList';
import {LithuanianCities} from './LithuanianCities';
//import State from 'pusher-js/types/src/core/http/state';

function useForceUpdate(){
    const [value, setValue] = useState(0);
    return () => setValue(value => value + 1);
}

export const FilterComponent = (props) => {

    const [rangeValues, setRangeValues] = useState([100, 500]);
    const [selectedCities, setSelectedCities] = useState([]);
    const [citiesVisible, setCitiesVisible] = useState(false);
    const forceUpdate = useForceUpdate();

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
            priceRange: [100, 500],
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

    const formik1 = useFormik({
        initialValues: {
            lowPrice: 0,
            highPrice: 1,
        },
        onSubmit: (values) => {
            console.log(values);
            axios({
                method: 'get',
                url: 'https://localhost:44332/listing/filter',
                params: {
                    lowPrice: rangeValues[0],
                    highPrice: rangeValues[1],
                }
            }).then((response) => {
                this.setState({listings: response.data})
            })
        }
    })

    const setParentClass = (toggle) => {
        setCitiesVisible(toggle);
    }

    const updateCheckboxList = (option) => {
        if (selectedCities.includes(option)) {
            let tempSelectedCities = selectedCities;
            const index = tempSelectedCities.indexOf(option);
            tempSelectedCities.splice(index, 1);
            setSelectedCities(tempSelectedCities);
        } else {
            var tempSelectedCities = selectedCities;
            tempSelectedCities.push(option);
            setSelectedCities(tempSelectedCities);
        }

        forceUpdate();
    }

    return (
        
        <div className={`filter-top-container${citiesVisible ? ' cities' : ''}`}>
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
            <label htmlFor="range">Price range</label>
            <Slider
                getAriaLabel={() => 'Price range'}
                value={rangeValues}
                min={0}
                max={2000}
                name="range"
                onChange={(e) => {setRangeValues(e.target.value); formik1.handleChange(e); formik1.submitForm();}}
                valueLabelDisplay="auto"
                style={{width: "200px"}}
            />
            <DropdownCheckboxList 
                options={LithuanianCities} 
                selectedOptions={selectedCities}
                setParentClass={setParentClass.bind(this)}
                updateCheckboxList={updateCheckboxList.bind(this)}
                />
            
        </div>
    );
}