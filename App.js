import { useState} from 'react';
import { StyleSheet, Text, View, Button, TextInput, ScrollView } from 'react-native';

export default function App() {
  const [enteredGoalText, setEnteredGoalText] = useState('');
  const [courseGoals, setCourseGoals] = useState([]);

  function goalInuputHandler (enteredText) {
    setEnteredGoalText(enteredText);
  }
  
  function addGoalHandler () {
    setCourseGoals((currentCourseGoals) => [
      ...currentCourseGoals,
      enteredGoalText,
    ]);
  }

  return (
    <View style={styles.appContainer}>
      <View style={styles.inputContainer}>
        <TextInput style={styles.textInput} placeholder='Your course goal!' onChangeText={goalInuputHandler}/>
        <Button title='Add Goal' onPress={addGoalHandler}/>
      </View>
      <View>
        <ScrollView>
          {courseGoals.map((goal) => <Text key={goal}>{goal}</Text>)}
        </ScrollView>
      </View>

    </View>
  ); 
}

const styles = StyleSheet.create({
  appContainer:{
    padding: 50,
  },
  inputContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  textInput:{
    borderWidth: 1,
    borderColor: 'green',
    width: '80%'
  }
});
