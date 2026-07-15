import static org.junit.jupiter.api.Assertions.*;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

class StudentTest {
    @Test
    @DisplayName("asdad")
    void Student(){
        Student s = new Student("", 30); //인스턴스

        //System.out.println(s.name);

        s.setName("진미");
        s.setAge(30);

        assertEquals("진미",s.getName(),"이름이 달라요.");
        assertEquals(30, s.getAge(),"나이가 달라요.");
    }

}