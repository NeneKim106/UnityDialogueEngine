using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 8f;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start() {
        // ���� ������Ʈ���� Rigidbody ������Ʈ�� ã�� bulletRigidbody�� �Ҵ�
        bulletRigidbody = GetComponent<Rigidbody>();
        // ������ٵ��� �ӵ� = ���ʹ��� * �̵� �ӷ�
        bulletRigidbody.velocity = transform.forward * speed;
        // 3�� �ڿ� �ڽ��� ���� ������Ʈ �ı�
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update() {

    }

    // Ʈ���� �浹 �� �ڵ����� ����Ǵ� �޼���
    private void OnTriggerEnter(Collider other) {
        // �յ��� ���� ���� ������Ʈ�� Player �±׸� ���� ���
        if (other.tag == "Player") {
            // ���� ���� ������Ʈ���� PlayerController ������Ʈ ��������
            PlayerController playerController = other.GetComponent<PlayerController>();
            // �������κ��� PlayerController ������Ʈ�� �������� �� �����ߴٸ�
            if (playerController != null) {
                // ���� PlayerController ������Ʈ�� Die() �޼��� ����
                playerController.Die();
            }
        }
    }
}
