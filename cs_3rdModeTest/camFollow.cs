/*
using UnityEngine;
public class camFollow : MonoBehaviour{    

    public Transform player;
    // 메라 위치 📌 이 코드도 동작은 잘되었지만 R키를 누르는 순간 카메라 이동이 안되었고 && 단순 이동시에도 카메라 이동이 안되어 아래 완전히 새로운 코드로 작성함📌
    // 카메라 거리와 위치
    public float distance = 4f;
    public float height = 1.5f;
    public float shoulderOffset = 0.5f;

    // 회전 속도
    public float mouseSpeed = 3f;
    public Joystick cameraJoystick;
    public float joystickCameraSpeed = 120f;

    // 현재 카메라 각도
    private float mouseX = 0f;
    private float mouseY = 15f;

    void LateUpdate(){
        // 마우스 입력
        float cameraX = Input.GetAxis("Mouse X") * mouseSpeed;
        float cameraY = Input.GetAxis("Mouse Y") * mouseSpeed;

        // 조이스틱 입력
        if (cameraJoystick != null){
            cameraX += cameraJoystick.Horizontal * joystickCameraSpeed * Time.deltaTime;
            cameraY += cameraJoystick.Vertical * joystickCameraSpeed * Time.deltaTime;
        }

        // 회전값 적용
        mouseX += cameraX;
        mouseY -= cameraY;

        // 상하 회전 제한
        mouseY = Mathf.Clamp(mouseY, -10f, 60f);

        // 바라볼 상체 위치
        Vector3 lookPosition = player.position;
        lookPosition.y += height;

        // 카메라 회전
        Quaternion cameraRotation = Quaternion.Euler(mouseY, mouseX, 0);

        // 플레이어 뒤쪽 위치
        Vector3 cameraPosition = lookPosition - cameraRotation * Vector3.forward * distance;

        // 오른쪽 어깨 위치
        cameraPosition += cameraRotation * Vector3.right * shoulderOffset;

        // 위치와 방향 적용
        transform.position = cameraPosition;
        transform.LookAt(lookPosition);
    }
}
*/

using UnityEngine;
public class camFollow : MonoBehaviour{

    public Transform player;
    // -------------------------------------- 카메라 설정 
    public float distance = 4f;
    public float height = 1.5f;
    public float mouseSpeed = 3f;
    public float touchSpeed = 0.15f;
    // -------------------------------------- 마지막으로 본 방향
    private float cameraX = 0f;
    private float cameraY = 15f;
    // -------------------------------------- 모바일 카메라 터치 번호 
    private int cameraFingerId = -1;

    void LateUpdate(){
        //🐊 1번째 🐊
        // -------------------------------------- PC 마우스 드래그
        if (Input.GetMouseButton(0)){
            cameraX -= Input.GetAxis("Mouse X") * mouseSpeed;
            cameraY += Input.GetAxis("Mouse Y") * mouseSpeed;
        }
        // -------------------------------------- 모바일 화면 드래그 :: 📌 Unity자체에 있는 phase, 화면을 터치한 손가락의 현 상태를 의미
        for (int i = 0; i < Input.touchCount; i++){
            Touch touch = Input.GetTouch(i);
            // 화면 오른쪽에서 시작한 터치 찾기
            if (cameraFingerId == -1 && touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2){
                cameraFingerId = touch.fingerId;
            }
            // 카메라 조작용 손가락
            if (touch.fingerId == cameraFingerId){
                if (touch.phase == TouchPhase.Moved){ 
                    cameraX -= touch.deltaPosition.x * touchSpeed;
                    cameraY += touch.deltaPosition.y * touchSpeed;
                }
                // 손가락을 뗐을 때
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
                    cameraFingerId = -1;
                }
            }
        }
        //🐊 2번째 🐊
        // 상하 회전 범위 제한
        cameraY = Mathf.Clamp(cameraY, -10f, 60f);

        // 카메라가 바라볼 플레이어 상체 위치
        Vector3 centerPosition = player.position;
        centerPosition.y += height;

        // 마우스·터치로 저장된 카메라 회전
        Quaternion cameraRotation = Quaternion.Euler(cameraY, cameraX, 0);

        // 플레이어를 중심으로 카메라 위치 계산
        Vector3 cameraPosition = centerPosition - cameraRotation * Vector3.forward * distance;

        // 계산한 위치와 방향 적용
        transform.position = cameraPosition;
        transform.LookAt(centerPosition);
}

// ⚠️ 🐊 2번째 🐊,🐊 2번째 🐊:: 입력 → 범위 제한 → 위치 계산 → 적용 순서:: 만약 순서가 바뀌면 카메라 반응이 한 프레임 늦어짐 ⚠️