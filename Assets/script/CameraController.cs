using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera initialCamera; // Kamera awal
    public Camera mainCamera; // Kamera utama

    public float transitionDuration = 2.0f; // Durasi perpindahan antara kamera awal dan kamera utama
    public float speed = 1.0f; // Kecepatan pergerakan kamera

    private bool isMoving = false; // Apakah kamera sedang bergerak

    void Start()
    {
        // Nonaktifkan kamera utama pada awal permainan
        mainCamera.enabled = false;
    }

    void Update()
    {
        // Jika tombol kiri mouse diklik dan kamera tidak sedang bergerak
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Mulai pergerakan kamera
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera()
    {
        isMoving = true; // Setel status sedang bergerak menjadi true

        // Simpan posisi awal dan rotasi awal kamera
        Vector3 startPos = initialCamera.transform.position;
        Quaternion startRot = initialCamera.transform.rotation;

        // Hitung jarak perjalanan
        float journeyLength = Vector3.Distance(startPos, mainCamera.transform.position);

        float startTime = Time.time; // Waktu mulai pergerakan

        while (Time.time - startTime < transitionDuration) // Lakukan pergerakan selama durasi transisi
        {
            float distCovered = (Time.time - startTime) * speed; // Hitung jarak yang telah ditempuh
            float fractionOfJourney = distCovered / journeyLength; // Hitung fraksi perjalanan

            // Pindahkan kamera secara lerengai ke titik tujuan
            initialCamera.transform.position = Vector3.Lerp(startPos, mainCamera.transform.position, fractionOfJourney);
            // Interpolasi rotasi kamera jika diperlukan
            initialCamera.transform.rotation = Quaternion.Lerp(startRot, mainCamera.transform.rotation, fractionOfJourney);

            yield return null; // Tunggu frame selanjutnya
        }

        // Pastikan kamera telah berada di posisi dan rotasi kamera utama
        initialCamera.transform.position = mainCamera.transform.position;
        initialCamera.transform.rotation = mainCamera.transform.rotation;

        // Nonaktifkan kamera awal
        initialCamera.enabled = false;

        // Aktifkan kamera utama
        mainCamera.enabled = true;

        isMoving = false; // Setel status sedang bergerak menjadi false setelah selesai bergerak
    }
}
